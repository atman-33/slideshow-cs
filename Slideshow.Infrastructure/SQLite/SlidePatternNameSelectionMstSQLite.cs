using System.Data.SQLite;
using Slideshow.Domain.Entities;
using Slideshow.Domain.Repositories;
using Slideshow.Domain.SQLite;

namespace Slideshow.Infrastructure.SQLite
{
    internal class SlidePatternNameSelectionMstSQLite : ISlidePatternNameSelectionMstRepository
    {
        public IReadOnlyList<SlidePatternNameSelectionMstEntity> GetData()
        {
            string sql = @"
SELECT
  slide_pattern_example_id,
  slide_pattern_name_example
FROM
  ssh_slide_pattern_name_selection_mst
";

            return SQLiteHelper.Query(sql,
                reader =>
                {
                    return new SlidePatternNameSelectionMstEntity(
                        Convert.ToInt32(reader["slide_pattern_example_id"]),
                        Convert.ToString(reader["slide_pattern_name_example"])
                        );
                });
        }

        public void Save(SlidePatternNameSelectionMstEntity entity)
        {
            string insert = @"
INSERT INTO ssh_slide_pattern_name_selection_mst
 (slide_pattern_example_id,
  slide_pattern_name_example)
VALUES
 (@slide_pattern_example_id,
  @slide_pattern_name_example)
";
            string update = @"
UPDATE ssh_slide_pattern_name_selection_mst
SET 
  slide_pattern_name_example = @slide_pattern_name_example
WHERE
  slide_pattern_example_id = @slide_pattern_example_id
";
            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@slide_pattern_example_id", entity.SlidePatternExampleId.Value),
                new SQLiteParameter("@slide_pattern_name_example", entity.SlidePatternNameExample.Value)
            };

            SQLiteHelper.Execute(insert, update, args.ToArray());
        }



        public void Delete(SlidePatternNameSelectionMstEntity entity)
        {
            string delete = @"
DELETE FROM ssh_slide_pattern_name_selection_mst WHERE slide_pattern_example_id = @slide_pattern_example_id
";

            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@slide_pattern_example_id", entity.SlidePatternExampleId.Value),
                new SQLiteParameter("@slide_pattern_name_example", entity.SlidePatternNameExample.Value)
            };

            SQLiteHelper.Execute(delete, args.ToArray());
        }
    }
}
