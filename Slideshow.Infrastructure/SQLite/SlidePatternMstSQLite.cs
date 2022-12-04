using System.Data.SQLite;
using Slideshow.Domain.Entities;
using Slideshow.Domain.Repositories;
using Slideshow.Domain.SQLite;

namespace Slideshow.Infrastruture.SQLite
{
    internal class SlidePatternMstSQLite : ISlidePatternMstRepository
    {
        public IReadOnlyList<SlidePatternMstEntity> GetData()
        {
            string sql = @"
SELECT
  slide_pattern_id,
  slide_pattern_name
FROM
  ssh_slide_pattern_mst
";

            return SQLiteHelper.Query(sql,
                reader =>
                {
                    return new SlidePatternMstEntity(
                        Convert.ToInt32(reader["slide_pattern_id"]),
Convert.ToString(reader["slide_pattern_name"])
);
                });
        }

        public void Save(SlidePatternMstEntity entity)
        {
            string insert = @"
INSERT INTO ssh_slide_pattern_mst
 (slide_pattern_id,
  slide_pattern_name)
VALUES
 (@slide_pattern_id,
  @slide_pattern_name)
";
            string update = @"
UPDATE ssh_slide_pattern_mst
SET 
  slide_pattern_name = @slide_pattern_name
WHERE
  slide_pattern_id = @slide_pattern_id
";
            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@slide_pattern_id", entity.SlidePatternId.Value),
new SQLiteParameter("@slide_pattern_name", entity.SlidePatternName.Value)
            };

            SQLiteHelper.Execute(insert, update, args.ToArray());
        }



        public void Delete(SlidePatternMstEntity entity)
        {
            string delete = @"
DELETE FROM ssh_slide_pattern_mst WHERE slide_pattern_id = @slide_pattern_id
";

            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@slide_pattern_id", entity.SlidePatternId.Value),
new SQLiteParameter("@slide_pattern_name", entity.SlidePatternName.Value)
            };

            SQLiteHelper.Execute(delete, args.ToArray());
        }
    }
}
