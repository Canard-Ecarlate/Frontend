namespace Models
{
    public class NbEachRole
    {
        public string RoleName { get; set; }
        public int Number { get; set; }

        public NbEachRole(string roleName, int number)
        {
            RoleName = roleName;
            Number = number;
        }
    }
}