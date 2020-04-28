using System;

namespace Data
{
    public class CategoryDTO
    {
        public Int32 Id { get; set; }

        
        public String Name { get; set; }

        
        public override Boolean Equals(Object obj)
        {
            return (obj is CategoryDTO dto) && Id == dto.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override String ToString()
        {
            return Name;
        }
    }
}
