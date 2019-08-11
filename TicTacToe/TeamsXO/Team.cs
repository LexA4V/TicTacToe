namespace TicTacToe.TeamsXO
{
    public abstract class Team
    {
        

        private XO _name;

        protected Team(XO name)
        {
            _name = name;
        }

        public XO Symbol
        { get { return _name; } }

    }


    public class TeamX : Team
    {
        public TeamX():base(XO.X)
        {

        }
    }

    public class TeamO : Team
    {
        public TeamO():base(XO.O)
        {

        }
    }
}
