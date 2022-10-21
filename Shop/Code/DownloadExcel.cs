
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ShopBase.Data;

namespace Shop.Code
{
    public class DownloadExcel: IDownload
    {
        ShopContext db;

        public DownloadExcel(
            ShopContext _db
            )
        {
            db = _db;
        }


        public void ImportSte(Stream stream)
        {
            XSSFWorkbook hssfwb;
            //using (FileStream file = new FileStream(local_file, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(stream);
            }
            ISheet sheet = hssfwb.GetSheetAt(0);
            var rowIndex = 0;
            var products=new List<Product>();
            var CategoryId = 0;
            var category = 0;
            var categoryList=new List<Category>();
            db.Categories.RemoveRange(db.Categories);
            db.Products.RemoveRange(db.Products);
            db.SaveChanges();
            while (sheet.GetRow(rowIndex) != null&&rowIndex<1000)
            {
                var categoryName = sheet.GetRow(rowIndex).GetCell(2).ToString();
                var checkCategory= categoryList.FirstOrDefault(x => x.Name == categoryName);
                if (checkCategory == null) {
                    CategoryId += 1;
                    category = CategoryId;
                    categoryList.Add(new Category { Id = CategoryId, Name = categoryName });
                }
                else {
                    category = checkCategory.Id;
                    //categoryList.Add(new Category { Id = category, Name = categoryName });
                }

                products.Add(new Product {
                    Id = Convert.ToInt32(sheet.GetRow(rowIndex).GetCell(0).ToString()),
                    Name= sheet.GetRow(rowIndex).GetCell(1).ToString(),
                    CodKpgz= sheet.GetRow(rowIndex).GetCell(3).ToString(),
                    Specifications = sheet.GetRow(rowIndex).GetCell(4).ToString(),
                    CategoryId=category
                });
                rowIndex++;
            }
            db.Categories.AddRange(categoryList);
            db.Products.AddRange(products);
            db.SaveChanges();
        }
    }
}
