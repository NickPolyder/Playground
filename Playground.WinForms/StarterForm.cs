using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Playground.WinForms.Database;

namespace Playground.WinForms
{
	public partial class StarterForm : Form
	{
		public StarterForm()
		{
			InitializeComponent();
		}

		private void GoBtn_Click(object sender, EventArgs e)
		{
			using (var db = new NorthwindEntities(true))
			{
				var categories = db.GetCatRolesByRoleIds(categoriesIdTextBox.Text);

				categoriesList.Items.Clear();
				foreach (var item in categories)
				{
					categoriesList.Items.Add(item.Description);
				}
			}
		}

		private void CategoriesIdTextBox_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
