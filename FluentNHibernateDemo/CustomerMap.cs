using FluentNHibernate.Mapping;

namespace FluentNHibernateDemo
{
    class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Table("Customer");
        }
    }
}