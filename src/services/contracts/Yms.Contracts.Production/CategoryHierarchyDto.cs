using System;
using System.Collections.Generic;

namespace Yms.Contracts.Production
{
    public class CategoryHierarchyDto
    {
        public CategoryHierarchyDto()
        {
            Items = new List<Hierarchy>();
        }
        public List<Hierarchy> Items { get; set; }
    }

    public class Hierarchy
    {
        public Hierarchy()
        {
            Items = new List<Hierarchy>();
        }
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<Hierarchy> Items { get; set; }

        public void AddAttribute(string key, string value)
        {
            if (Attributes == null)
            {
                Attributes = new Dictionary<string, string>();
            }

            Attributes[key] = value;
        }
    }
}