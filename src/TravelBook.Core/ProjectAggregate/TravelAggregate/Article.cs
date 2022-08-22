namespace TravelBook.Core.ProjectAggregate;

public class Article: Entity
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int TravelId { get; set; }
    public Travel? Travel { get; set; }

    public Article(){}

    public Article(string title, string text, int travelId)
    {
        Title = title;
        Text = text;
        TravelId = travelId;
    }
}

