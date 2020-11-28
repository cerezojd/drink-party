using DrinkParty.Features;

namespace DrinkParty.Entities
{
    public class RoomQuestion
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
