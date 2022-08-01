using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT12GenTree.Models
{
    public abstract class Adult : Child
    {
        private readonly List<Child> _children;

        protected Adult(string? name, bool isMan, Date birthDay, Man? father, Woman? mother) : base(name, isMan, birthDay, father, mother)
        {
            _children = new();
        }

        public Child[] Children => _children.ToArray();
        public void AddChild(Child child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (child.Mother != this && this is Woman mother)
            {
                child.Mother = mother;
            }
            else if (child.Father != this && this is Man father)
            {
                child.Father = father;
            }

            _children.Add(child);
        }

        public void RemoveChild(Child child) 
        {
            if (child != null)
            {
                if (child.Father == this)
                {
                    child.Father = null;
                }

                if (child.Mother == this)
                {
                    child.Mother = null;
                }

                _children.Remove(child);
            }
        }

        public override string ToString() => base.ToString() + $"Childern: {_children.Count}.";
    }
}
