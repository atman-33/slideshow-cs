using System.Data.SQLite;
using Slideshow.Domain.Entities;
using Slideshow.Domain.Repositories;
using Slideshow.Domain.SQLite;

namespace Slideshow.Infrastructure.SQLite
{
    internal class SlidePatternPageMstSQLite : ISlidePatternPageMstRepository
    {
        public IReadOnlyList<SlidePatternPageMstEntity> GetData()
        {
            string sql = @"
SELECT
  slide_pattern_id,
  slide_no,
  page_id
FROM
  ssh_slide_pattern_page_mst
";

            return SQLiteHelper.Query(sql,
                reader =>
                {
                    return new SlidePatternPageMstEntity(
                        Convert.ToInt32(reader["slide_pattern_id"]),
                        Convert.ToInt32(reader["slide_no"]),
                        Convert.ToInt32(reader["page_id"])
                        );
                });
        }

        public void Save(SlidePatternPageMstEntity entity)
        {
            string insert = @"
INSERT INTO ssh_slide_pattern_page_mst
 (slide_pattern_id,
  slide_no,
  page_id)
VALUES
 (@slide_pattern_id,
  @slide_no,
  @page_id)
";
            string update = @"
UPDATE ssh_slide_pattern_page_mst
SET 
  page_id = @page_id
WHERE
  slide_pattern_id = @slide_pattern_id
  AND slide_no = @slide_no
";
            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@slide_pattern_id", entity.SlidePatternId.Value),
                new SQLiteParameter("@slide_no", entity.SlideNo.Value),
                new SQLiteParameter("@page_id", entity.PageId.Value)
            };

            SQLiteHelper.Execute(insert, update, args.ToArray());
        }



        public void Delete(SlidePatternPageMstEntity entity)
        {
            string delete = @"
DELETE FROM ssh_slide_pattern_page_mst WHERE slide_pattern_id = @slide_pattern_id
  AND slide_no = @slide_no
";

            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@slide_pattern_id", entity.SlidePatternId.Value),
                new SQLiteParameter("@slide_no", entity.SlideNo.Value),
                new SQLiteParameter("@page_id", entity.PageId.Value)
            };

            SQLiteHelper.Execute(delete, args.ToArray());
        }
    }
}
