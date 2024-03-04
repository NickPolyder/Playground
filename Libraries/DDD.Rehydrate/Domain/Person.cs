namespace DDD.Rehydrate.Domain
{
    public class Person : DomainEntity, IRehydrate<IEnumerable<License>>
    {
        public string FullName { get; }

        public string Email { get; }

        private List<License> _licenses;

        public IReadOnlyCollection<License> Licenses => _licenses.AsReadOnly();

        public Person(string id, string fullName, string email) : base(id)
        {
            FullName = fullName;
            Email = email;
            _licenses = new List<License>();
        }

        public void Rehydrate(IEnumerable<License> entities)
        {
            _licenses.Clear();
            _licenses.AddRange(entities);
        }
    }
}
