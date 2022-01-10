using CafeAutomationCodeFirst.Data;
using CafeAutomationCodeFirst.Models;
using CafeAutomationCodeFirst.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeAutomationCodeFirst.Forms
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private CafeContext cafeContext = new CafeContext();
        private CategoryRepository categoryRepository = new CategoryRepository();
        private ProductRepository productRepository = new ProductRepository();

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            GetCategories();
            GetProducts();
        }

        private void GetCategories()
        {
            lstCategory.DataSource = null;
            lstCategory.DataSource = categoryRepository.Get(x=>x.IsDeleted == false).ToList();
        }

        private void GetProducts()
        {
            lstProduct.DataSource = null;
            lstProduct.DataSource = productRepository.Get()
                .Where(x => x.CategoryId == selectedCategory.Id && x.IsDeleted == false)
                .ToList();
        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            var category = new Category()
            {
                CategoryName = txtCategoryName.Text,
                Description = txtDescription.Text,
                IsDeleted = false
            };
            if(pbCategory.Image != null)
            {
                MemoryStream resimStream = new MemoryStream();
                pbCategory.Image.Save(resimStream, ImageFormat.Jpeg);

                category.CategoryPicture = resimStream.ToArray();
            }
            categoryRepository.Add(category);
            GetCategories();
        }

        private Category selectedCategory;
        private void lstCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCategory.SelectedItem == null) return;

            selectedCategory = lstCategory.SelectedItem as Category;
            txtCategoryName.Text = selectedCategory.CategoryName;
            txtDescription.Text = selectedCategory.Description;

            if(selectedCategory.CategoryPicture != null)
            {
                MemoryStream stream = new MemoryStream(selectedCategory.CategoryPicture);
                pbCategory.Image = Image.FromStream(stream);
            }
            else
            {
                pbCategory.Image = null;
            }
            GetProducts();
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            var product = new Product()
            {
                ProductName = txtProductName.Text,
                Price = nFiyat.Value,
                IsDeleted = false
            };
            if (pbProduct.Image != null)
            {
                MemoryStream resimStream = new MemoryStream();
                pbProduct.Image.Save(resimStream, ImageFormat.Jpeg);

                product.ProductPicture = resimStream.ToArray();
            }
            categoryRepository.AddProduct(selectedCategory, product);
            GetProducts();
        }

        private void pbCategory_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Bir fotoğraf seçiniz";
            dialog.Filter = "Resim Dosyaları | *.jpeg; *.jpg";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pbCategory.ImageLocation = dialog.FileName;
            }
        }

        private void pbProduct_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Bir fotoğraf seçiniz";
            dialog.Filter = "Resim Dosyaları | *.jpeg; *.jpg";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pbProduct.ImageLocation = dialog.FileName;
            }
        }

        private Product selectedProduct;
        private void lstProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProduct.SelectedItem == null) return;

            selectedProduct = lstProduct.SelectedItem as Product;
            txtProductName.Text = selectedProduct.ProductName;
            nFiyat.Value = selectedProduct.Price;
            if (selectedProduct.ProductPicture != null)
            {
                MemoryStream stream = new MemoryStream(selectedProduct.ProductPicture);
                pbProduct.Image = Image.FromStream(stream);
            }
            else
            {
                pbProduct.Image = null;
            }
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCategory == null) return;
            selectedCategory.CategoryName = txtCategoryName.Text;
            selectedCategory.Description = txtDescription.Text;
            if (pbCategory.Image != null)
            {
                MemoryStream resimStream = new MemoryStream();
                pbCategory.Image.Save(resimStream, ImageFormat.Jpeg);

                selectedCategory.CategoryPicture = resimStream.ToArray();
            }
            categoryRepository.Update(selectedCategory);
            GetCategories();
        }

        private void btnProductUpdate_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null) return;
            selectedProduct.ProductName = txtProductName.Text;
            selectedProduct.Price = nFiyat.Value;
            if (pbProduct.Image != null)
            {
                MemoryStream resimStream = new MemoryStream();
                pbProduct.Image.Save(resimStream, ImageFormat.Jpeg);

                selectedProduct.ProductPicture = resimStream.ToArray();
            }
            productRepository.Update(selectedProduct);
            GetProducts();
        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            if (selectedCategory == null) return;
            selectedCategory.IsDeleted = true;
            categoryRepository.Update(selectedCategory);
            GetCategories();
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null) return;
            selectedProduct.IsDeleted = true;
            productRepository.Update(selectedProduct);
            GetProducts();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
