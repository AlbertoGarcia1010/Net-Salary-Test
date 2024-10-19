namespace Salary.App.Utils
{
    public class ListDataModel
    {
        public ListDataModel()
        {
        }

        public ListDataModel(object data, int totalrows, int rowsfiltered)
        {
            this.data = data;
            this.totalrows = totalrows;
            this.rowsfiltered = rowsfiltered;
        }


        public object data { get; set; }
        public int totalrows { get; set; }
        public int rowsfiltered { get; set; }
    }
}
