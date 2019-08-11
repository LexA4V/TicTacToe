namespace TicTacToe.TeamsXO
{
    public class Side
    {
        private TeamX _teamX;
        private TeamO _teamO;

        private Team _currentTeam;

        public Team Current { get { return _currentTeam; } }

        protected Side()
        {
            _teamX = new TeamX();
            _teamO = new TeamO();
            _currentTeam = _teamX;
        }

        public static Side Create()
        {
            return new Side();
        }

        internal void Reset()
        {
            _currentTeam = _teamX; 
        }

        internal void changeTeam()
        {
            if (_currentTeam == _teamX) _currentTeam = _teamO;
            else if (_currentTeam == _teamO) _currentTeam = _teamX;
        }
    }
}
