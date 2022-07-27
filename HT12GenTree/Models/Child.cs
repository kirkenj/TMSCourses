using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT12GenTree.Models
{
    internal class Child : Person
    {
        Man? _father;
        Woman? _mother;
        public readonly bool IsMan;

        public Child(string? name, bool isMan, Date birthDay, Man? father, Woman? mother) : base(name, birthDay)
        {
            IsMan = isMan;
            Father = father;
            Mother = mother;
        }

        public Man? Father
        {
            get => _father;
            set => SetParent(value, true);
        }

        public Woman? Mother
        {
            get => _mother;
            set => SetParent(value, false);
        }

        private void SetParent(Adult? newParent, bool doSetOnFather)
        {
            if (newParent != null && (this.BirthDate - newParent.BirthDate).Years < Person.MINIMAL_AGE_FOR_PARENTIONG)
            {
                throw new ArgumentException("This person is too young", nameof(newParent));
            }

            Adult? exParent;
            if (doSetOnFather) 
            {
                exParent = _father;
                _father = newParent == null ? null : (Man)newParent;
            }
            else 
            {
                exParent= _mother;
                _mother = newParent == null ? null : (Woman)newParent;
            }

            if (exParent != null)
            {
                exParent.RemoveChild(this);
            }

            if (newParent != null)
            {
                newParent.AddChild(this);
            }
        }

        public override string ToString() => base.ToString() + $"{(IsMan ? "Man" : "Woman")}. Father: {(_father == null ? "No data" : _father.Name)}. Mother: {(_mother == null ? "No data" : _mother.Name)}. ";
    }
}
