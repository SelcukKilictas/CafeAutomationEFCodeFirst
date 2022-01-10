using CafeAutomationCodeFirst.Data;
using CafeAutomationCodeFirst.Models;
using CafeAutomationCodeFirst.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeAutomationCodeFirst.Forms
{
    public partial class FrmFloorSetting : Form
    {
        public FrmFloorSetting()
        {
            InitializeComponent();
        }

        private CafeContext cafeContext = new CafeContext();
        private FloorRepository floorRepository = new FloorRepository();
        private TableRepository tableRepository = new TableRepository();
        private OrderRepository orderRepository = new OrderRepository();

        private void FrmFloorSetting_Load(object sender, EventArgs e)
        {
            GetFloors();
        }

        private void GetFloors()
        {
            lstFloor.DataSource = null;
            lstFloor.DataSource = floorRepository.Get(x=>x.IsDeleted == false).OrderBy(x => x.FloorOrder)
                .ToList();
        }

        private void btnFloorAdd_Click(object sender, EventArgs e)
        {
            Floor newFloor = new Floor()
            {
                FloorName = txtFloorName.Text,
                Word = txtWord.Text,
                FloorOrder = Convert.ToInt32(txtFloorOrder.Text),
                TableCount = Convert.ToInt32(txtTableCount.Text),
                IsDeleted = false
            };

            for (int i = 0; i < newFloor.TableCount; i++)
            {
                Table newTable = new Table()
                {
                    TableOrder = i + 1,
                    TableName = $"{newFloor.Word}/Masa {i + 1}",
                    TableStatus = false,
                    IsDeleted = false
                };
                newFloor.Tables.Add(newTable);
            }

            floorRepository.Add(newFloor);
            GetFloors();
        }

        private Floor selectedFloor;
        private void lstFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFloor.SelectedItem == null) return;

            selectedFloor = lstFloor.SelectedItem as Floor;
            txtFloorName.Text = selectedFloor.FloorName;
            txtFloorOrder.Text = selectedFloor.FloorOrder.ToString();
            txtWord.Text = selectedFloor.Word;
            txtTableCount.Text = selectedFloor.TableCount.ToString();

            //var liste = tableRepository.Get().Where(x => x.FloorId == selectedFloor.Id).ToList();
            //txtTableCount.Text = liste.Count().ToString();

        }

        private bool varMi = false;
        private void btnFloorDelete_Click(object sender, EventArgs e)
        {
            if (selectedFloor == null) return;
            

            List<Table> tables = tableRepository.Get().Where(x => x.FloorId == selectedFloor.Id && x.IsDeleted == false).OrderBy(x => x.TableOrder).ToList();
            
            foreach (Table table in tables)
            {
                var control = orderRepository.Get().FirstOrDefault(x => x.TableId == table.Id && x.OrderStatus == true && x.IsDeleted == false);
                if (control != null)
                {
                    varMi = true;
                    break;
                }
                else
                {
                    varMi = false;
                }
            }

            if(varMi == true)
            {
                MessageBox.Show("Silmek İstediğiniz Katta Aktif Sipariş Var!!!");
            }
            else
            {
                foreach (Table table in tables)
                {
                    table.IsDeleted = true;
                    tableRepository.Update(table);
                }
                selectedFloor.IsDeleted = true;
                floorRepository.Update(selectedFloor);
            }
            GetFloors();
        }

        private void btnFloorUpdate_Click(object sender, EventArgs e)
        {
            if (selectedFloor == null) return;

            List<Table> tables = tableRepository.Get().Where(x => x.FloorId == selectedFloor.Id && x.IsDeleted == false).OrderBy(x => x.TableOrder).ToList();
            foreach (Table table in tables)
            {
                table.IsDeleted = true;
                tableRepository.Update(table);
            }

            selectedFloor.FloorName = txtFloorName.Text;
            selectedFloor.FloorOrder = Convert.ToInt32(txtFloorOrder.Text);
            selectedFloor.Word = txtWord.Text;
            selectedFloor.TableCount = Convert.ToInt32(txtTableCount.Text);

            for (int i = 0; i < selectedFloor.TableCount; i++)
            {
                Table newTable = new Table()
                {
                    TableOrder = i + 1,
                    TableName = $"{selectedFloor.Word}/Masa {i + 1}",
                    TableStatus = false,
                    IsDeleted = false
                };
                selectedFloor.Tables.Add(newTable);
            }
            tableRepository.Save();
            floorRepository.Update(selectedFloor);
            floorRepository.Save();
            GetFloors();
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
