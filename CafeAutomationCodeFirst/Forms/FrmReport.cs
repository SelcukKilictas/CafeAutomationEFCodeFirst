using CafeAutomationCodeFirst.Data;
using CafeAutomationCodeFirst.Repository;
using CafeAutomationCodeFirst.ViewModels;
using Microsoft.EntityFrameworkCore;
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
    public partial class FrmReport : Form
    {
        public FrmReport()
        {
            InitializeComponent();
        }

        private CafeContext cafeContext = new CafeContext();
        private CategoryRepository categoryRepository = new CategoryRepository();
        private ProductRepository productRepository = new ProductRepository();
        private OrderRepository orderRepository = new OrderRepository();
        private TableRepository tableRepository = new TableRepository();

        private void FrmReport_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd/MM/yyyy";
            DayReport();
            FilterLoad();
            
        }

        private decimal toplam = 0;
        private void DayReport()
        {
            lblDay.Text = $"{dateTimePicker1.Value.ToString("dd/MM/yyyy")} GÜNÜ TOPLAM CİROSU :";

            var query = from ord in cafeContext.Orders
                        join prod in cafeContext.Products on ord.ProductId equals prod.Id
                        join tbl in cafeContext.Tables on ord.TableId equals tbl.Id
                        select new ReportViewModel()
                        {
                            //OrderId = ord.Id,
                            //ProductId = ord.ProductId,
                            TableName = tbl.TableName,
                            ProductName = prod.ProductName,
                            SubTotal = ord.SubTotal,
                            //TableId = ord.TableId,
                            OrderStatus = ord.OrderStatus,
                            DateTime = ord.DateTime,
                            DateTimeDay = ord.DateTimeDay,
                            DateTimeHour = ord.DateTimeHour
                        };

            var liste = query.Where(x => x.DateTimeDay == dateTimePicker1.Value.ToString("dd/MM/yyyy") && x.OrderStatus == false).OrderByDescending(x=>x.DateTimeHour).ToList();
            dgvOrders.DataSource = null;
            dgvOrders.DataSource = liste;
            dgvOrders.Columns["OrderStatus"].Visible = false;
            dgvOrders.Columns["DateTimeDay"].Visible = false;
            dgvOrders.Columns["DateTimeHour"].Visible = false;
            dgvOrders.Columns[0].HeaderText = "MASA İSMİ";
            dgvOrders.Columns[1].HeaderText = "ÜRÜN ADI";
            dgvOrders.Columns[2].HeaderText = "ARA TOPLAM";
            dgvOrders.Columns[4].HeaderText = "TARİH";

            toplam = 0;
            foreach (var item in liste)
            {
                toplam += item.SubTotal;
            }
            lblTotalPrice.Text = $"{toplam}₺";
        }



        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DayReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FilterLoad()
        {
            #region
            //var query = from ord in cafeContext.Orders
            //            join prod in cafeContext.Products on ord.ProductId equals prod.Id
            //            join tbl in cafeContext.Tables on ord.TableId equals tbl.Id
            //            select new ReportViewModel()
            //            {
            //                //OrderId = ord.Id,
            //                //ProductId = ord.ProductId,
            //                TableName = tbl.TableName,
            //                ProductName = prod.ProductName,
            //                SubTotal = ord.SubTotal,
            //                //TableId = ord.TableId,
            //                OrderStatus = ord.OrderStatus,
            //                DateTime = ord.DateTime,
            //                DateTimeDay = ord.DateTimeDay,
            //                DateTimeHour = ord.DateTimeHour
            //};
            #endregion
            //Group by öncesi

            var query2 = from ord in cafeContext.Orders
                         join prod in cafeContext.Products on ord.ProductId equals prod.Id
                         join tbl in cafeContext.Tables on ord.TableId equals tbl.Id
                         where ord.DateTimeDay == dateTimePicker2.Value.ToString("dd/MM/yyyy") && ord.OrderStatus == false
                         group new { ord, prod, tbl } by new
                         {
                             prod.ProductName,
                         } into list
                         orderby list.Key.ProductName
                         select new ReportViewModel
                         {
                             ProductName = list.Key.ProductName,
                             SubTotal = list.Sum(x => x.ord.SubTotal),
                         };

            var liste = query2.ToList();
            //var liste = query.Where(x => x.DateTime >= dateTimePicker2.Value && x.DateTime <= dateTimePicker3.Value).OrderByDescending(x => x.DateTimeDay).ThenByDescending(x => x.DateTimeHour).ToList();

            dgvMonthReport.DataSource = null;
            dgvMonthReport.DataSource = liste;
            dgvMonthReport.Columns["OrderStatus"].Visible = false;
            dgvMonthReport.Columns["TableName"].Visible = false;
            dgvMonthReport.Columns["DateTime"].Visible = false;
            dgvMonthReport.Columns["DateTimeDay"].Visible = false;
            dgvMonthReport.Columns["DateTimeHour"].Visible = false;
            dgvMonthReport.Columns[1].HeaderText = "ÜRÜN ADI";
            dgvMonthReport.Columns[2].HeaderText = "TOPLAM SATIŞ";

            toplam = 0;
            foreach (var item in liste)
            {
                toplam += item.SubTotal;
            }
            lblFilterPrice.Text = $"SEÇİLİ GÜNLER ARASI TOPLAM CİRO :     {toplam}₺";

        }  //Form Yüklenirken O Güne Ait Rapor
        
        
        private void MonthFilter()
        {
            #region
            //var query = from ord in cafeContext.Orders
            //            join prod in cafeContext.Products on ord.ProductId equals prod.Id
            //            join tbl in cafeContext.Tables on ord.TableId equals tbl.Id
            //            select new ReportViewModel()
            //            {
            //                //OrderId = ord.Id,
            //                //ProductId = ord.ProductId,
            //                TableName = tbl.TableName,
            //                ProductName = prod.ProductName,
            //                SubTotal = ord.SubTotal,
            //                //TableId = ord.TableId,
            //                OrderStatus = ord.OrderStatus,
            //                DateTime = ord.DateTime,
            //                DateTimeDay = ord.DateTimeDay,
            //                DateTimeHour = ord.DateTimeHour
            //};
            #endregion
            //Group by öncesi
            
            //İki Tarihin Aynı Olduğu Kontrol....
            if (dateTimePicker2.Value.ToString("dd/MM/yyyy") == dateTimePicker3.Value.ToString("dd/MM/yyyy"))
            {
                var query3 = from ord in cafeContext.Orders
                             join prod in cafeContext.Products on ord.ProductId equals prod.Id
                             join tbl in cafeContext.Tables on ord.TableId equals tbl.Id
                             where ord.DateTimeDay == dateTimePicker2.Value.ToString("dd/MM/yyyy") && ord.OrderStatus == false
                             group new { ord, prod, tbl } by new
                             {
                                 prod.ProductName,
                             } into list
                             orderby list.Key.ProductName
                             select new ReportViewModel
                             {
                                 ProductName = list.Key.ProductName,
                                 SubTotal = list.Sum(x => x.ord.SubTotal),
                             };

                var liste2 = query3.ToList();
                dgvMonthReport.DataSource = null;
                dgvMonthReport.DataSource = liste2;
                dgvMonthReport.Columns["OrderStatus"].Visible = false;
                dgvMonthReport.Columns["TableName"].Visible = false;
                dgvMonthReport.Columns["DateTime"].Visible = false;
                dgvMonthReport.Columns["DateTimeDay"].Visible = false;
                dgvMonthReport.Columns["DateTimeHour"].Visible = false;
                dgvMonthReport.Columns[1].HeaderText = "ÜRÜN ADI";
                dgvMonthReport.Columns[2].HeaderText = "TOPLAM SATIŞ";

                toplam = 0;
                foreach (var item in liste2)
                {
                    toplam += item.SubTotal;
                }
                lblFilterPrice.Text = $"SEÇİLİ GÜNLER ARASI TOPLAM CİRO :     {toplam}₺";
            }
            else
            {
                var query2 = from ord in cafeContext.Orders
                             join prod in cafeContext.Products on ord.ProductId equals prod.Id
                             join tbl in cafeContext.Tables on ord.TableId equals tbl.Id
                             where ord.DateTime >= dateTimePicker2.Value && ord.DateTime <= dateTimePicker3.Value && ord.OrderStatus == false
                             group new { ord, prod, tbl } by new
                             {
                                 prod.ProductName,
                             } into list
                             orderby list.Key.ProductName
                             select new ReportViewModel
                             {
                                 ProductName = list.Key.ProductName,
                                 SubTotal = list.Sum(x => x.ord.SubTotal),
                             };

                var liste = query2.ToList();


                dgvMonthReport.DataSource = null;
                dgvMonthReport.DataSource = liste;
                dgvMonthReport.Columns["OrderStatus"].Visible = false;
                dgvMonthReport.Columns["TableName"].Visible = false;
                dgvMonthReport.Columns["DateTime"].Visible = false;
                dgvMonthReport.Columns["DateTimeDay"].Visible = false;
                dgvMonthReport.Columns["DateTimeHour"].Visible = false;
                dgvMonthReport.Columns[1].HeaderText = "ÜRÜN ADI";
                dgvMonthReport.Columns[2].HeaderText = "TOPLAM SATIŞ";

                toplam = 0;
                foreach (var item in liste)
                {
                    toplam += item.SubTotal;
                }
                lblFilterPrice.Text = $"SEÇİLİ GÜNLER ARASI TOPLAM CİRO :     {toplam}₺";
            }
            
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            MonthFilter();
        }
    }
}
