
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ShopBase.Data;

namespace Shop.Code
{
    public class DownloadExcel: IDownload
    {
        bugZillaContext db;

        public DownloadExcel(
            bugZillaContext _db
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
            var rowIndex = 1;
            var products=new List<Log>();
            //db.Logs.RemoveRange(db.Logs);
            //db.SaveChanges();
            while (sheet.GetRow(rowIndex) != null
                //&&rowIndex<45000
                )
            {

                products.Add(new Log {
                    Id = Guid.NewGuid(),
                    LogId= sheet.GetRow(rowIndex).GetCell(0).ToString(),
                    Date= Convert.ToDateTime(sheet.GetRow(rowIndex).GetCell(1).ToString()),
                    Text = sheet.GetRow(rowIndex).GetCell(2).ToString(),
                });
                rowIndex++;
            }
            //db.Logs.AddRange(products);
            var uniqProduct = new List<Log>();
            var prevText = "";
            foreach(var item in products.OrderBy(x=>x.Text)) {
                if (item.Text.Length > 30)
                {
                    var newText = item.Text.Substring(0, 30);
                    if (newText != prevText)
                    {
                        uniqProduct.Add(item);
                        prevText = newText;
                    }
                }
                else
                {
                    var newText= item.Text;
                    if (newText != prevText)
                    {
                        uniqProduct.Add(item);
                        prevText = newText;
                    }
                }
            }
            db.SaveChanges();
        }
    }
}
