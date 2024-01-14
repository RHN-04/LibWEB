using System.ComponentModel.DataAnnotations;

namespace LibWEB.ViewModel
{
    public class BookViewModel
    {
            public int Id { get; set; }
            [Display(Name = "Название книги")]
            public string Title { get; set; } = null!;
            [Display(Name = "Возрастное ограничение")]
            public int AgeRestriction { get; set; }
            [Display(Name = "Год выпуска")]
            public int YearOfPublishing { get; set; }
            [Display(Name = "Краткое описание")]
            public string Description { get; set; } = null!;
            [Display(Name = "Количество")]
            public int Numbers { get; set; }
            public string? ImageId { get; set; }

            public string GetImageUrl()
            {
                if (string.IsNullOrEmpty(ImageId))
                {
                    return string.Empty;
                }

                return $"images/{ImageId}";
            }
            public List<AuthorViewModel> SelectedAuthors { get; set; } = new List<AuthorViewModel>();
            public List<string> SelectedGenres { get; set; } = new List<string>();

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                BookViewModel other = (BookViewModel)obj;

                return Id == other.Id &&
                   SelectedAuthors.SequenceEqual(other.SelectedAuthors) &&
                   SelectedGenres.SequenceEqual(other.SelectedGenres);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = 17;
                    hashCode = hashCode * 23 + Id.GetHashCode();

                    foreach (var author in SelectedAuthors)
                    {
                        hashCode = hashCode * 23 + author.GetHashCode();
                    }

                    foreach (var genre in SelectedGenres)
                    {
                        hashCode = hashCode * 23 + genre.GetHashCode();
                    }

                    return hashCode;
                }
            }
    }
}
