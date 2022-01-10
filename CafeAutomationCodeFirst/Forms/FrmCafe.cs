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
    public partial class FrmCafe : Form
    {
        public FrmCafe()
        {
            InitializeComponent();
        }

        private CafeContext cafeContext = new CafeContext();
        private FloorRepository floorRepository = new FloorRepository();
        private TableRepository tableRepository = new TableRepository();
        private OrderRepository orderRepository = new OrderRepository();

        Color selectedFloorColor = Color.Coral, defaultFloorColor = Color.CornflowerBlue;

        private void FrmCafe_Load(object sender, EventArgs e)
        {
            List<Floor> floors = floorRepository.Get(x=>x.IsDeleted == false).OrderBy(x=>x.FloorOrder).ToList();

            for (int i = 0; i < floors.Count; i++)
            {
                Floor floor = floors[i];
                Button btn = new Button()
                {
                    Text = floor.FloorName,
                    Size = new Size(250,125),
                    BackColor = defaultFloorColor,
                    Tag = floor
                };
                btn.Click += KatBtn_Click;
                flpFloors.Controls.Add(btn);
            }

            foreach (Button button in flpFloors.Controls)  //İlk katın 
            {
                button.PerformClick();
                break;
            }

        }

        private Floor selectedFloor;
        private void KatBtn_Click(object sender,EventArgs e)
        {
            Button selectedButton = sender as Button;
            selectedFloor = selectedButton.Tag as Floor;

            //List<Table> tables = selectedFloor.Tables.ToList();
            List<Table> tables = tableRepository.Get().Where(x => x.FloorId == selectedFloor.Id && x.IsDeleted == false).OrderBy(x=>x.TableOrder).ToList();


            flpTables.Controls.Clear();
            foreach (Table table in tables)
            {
                Button btn = new Button()
                {
                    Text = table.TableName,
                    Size = new Size(250, 125),
                    BackColor = defaultFloorColor,
                    Tag = table
                };
                btn.Click += MasaBtn_Click;
                flpTables.Controls.Add(btn);
            }

            foreach (Button button in flpFloors.Controls)
            {
                button.BackColor = defaultFloorColor;
                if(button.Text == selectedButton.Text)
                {
                    button.BackColor = selectedFloorColor;
                }
            }

            TableColorControl();
            
        }

        public Table controlTable;
        public void TableColorControl()
        {
            //Masanın Siparişleri Varsa Renklendir Ona Göre
            foreach (Button button in flpTables.Controls)
            {
                controlTable = button.Tag as Table;
                var control = orderRepository.Get().FirstOrDefault(x => x.TableId == controlTable.Id && x.OrderStatus == true);
                if(control != null)
                {
                    button.BackColor = Color.Red;
                }
            }
        }

        private FrmOrder frmOrder;

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MasaBtn_Click(object sender, EventArgs e)
        {
            Button selectedButton = sender as Button;
            if (frmOrder == null || frmOrder.IsDisposed)
            {
                frmOrder = new FrmOrder(selectedButton.Tag as Table);
            }
            frmOrder.Show();
            frmOrder.WindowState = FormWindowState.Maximized;
            Close();
            //frmOrder.selectedTable = selectedButton.Tag as Table;  //Formun Loadında Gönderdik....
        }

    }
}
