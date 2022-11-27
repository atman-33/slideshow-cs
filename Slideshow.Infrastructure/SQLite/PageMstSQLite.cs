using Slideshow.Domain.Entities;
using Slideshow.Domain.Repositories;
using Slideshow.Domain.SQLite;
using System.Data.SQLite;

namespace Template.Infrastruture.SQLite
{
    internal class PageMstSQLite : IPageMstRepository
    {
        public IReadOnlyList<PageMstEntity> GetData()
        {
            string sql = @"
SELECT
  page_id,
  page_name,
  movie_link,
  image_link,
  image_page_no,
  slide_waiting_time,
  note1,
  note2,
  note3
FROM
  ssh_page_mst
";

            return SQLiteHelper.Query(sql,
                reader =>
                {
                    return new PageMstEntity(
                        Convert.ToInt32(reader["page_id"]),
                        Convert.ToString(reader["page_name"]),
                        reader["movie_link"] != DBNull.Value ? Convert.ToString(reader["movie_link"]) : null,
                        reader["image_link"] != DBNull.Value ? Convert.ToString(reader["image_link"]) : null,
                        reader["image_page_no"] != DBNull.Value ? Convert.ToInt32(reader["image_page_no"]) : null,
                        Convert.ToSingle(reader["slide_waiting_time"]),
                        reader["note1"] != DBNull.Value ? Convert.ToString(reader["note1"]) : null,
                        reader["note2"] != DBNull.Value ? Convert.ToString(reader["note2"]) : null,
                        reader["note3"] != DBNull.Value ? Convert.ToString(reader["note3"]) : null
                        );
                });
        }

        public void Save(PageMstEntity entity)
        {
            string insert = @"
insert into ssh_page_mst
(page_id, page_name, movie_link, image_link, image_page_no, slide_waiting_time, note1, note2, note3)
values
(@page_id, @page_name, @movie_link, @image_link, @image_page_no, @slide_waiting_time, @note1, @note2, @note3)
";
            string update = @"
update ssh_page_mst
set 
  page_name = @page_name,
  movie_link = @movie_link,
  image_link = @image_link,
  image_page_no = @image_page_no,
  slide_waiting_time = @slide_waiting_time,
  note1 = @note1,
  note2 = @note2,
  note3 = @note3

where
  page_id = @page_id
";
            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@page_id", entity.PageId.Value),
                new SQLiteParameter("@page_name", entity.PageName.Value),
                new SQLiteParameter("@movie_link", entity.MovieLink.Value),
                new SQLiteParameter("@image_link", entity.ImageLink.Value),
                new SQLiteParameter("@image_page_no", entity.ImagePageNo.Value),
                new SQLiteParameter("@slide_waiting_time", entity.SlideWaitingTime.Value),
                new SQLiteParameter("@note1", entity.Note1.Value),
                new SQLiteParameter("@note2", entity.Note2.Value),
                new SQLiteParameter("@note3", entity.Note3.Value),
            };

            SQLiteHelper.Execute(insert, update, args.ToArray());
        }
    }
}
