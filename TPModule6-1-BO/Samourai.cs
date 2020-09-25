using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TpModule6Bo
{
    public class Samourai : Ids
    {
        public override int Id { get; set; }
        public int Force { get; set; }
        public override string Nom { get; set; }
        public virtual Arme Arme { get; set; }
        [DisplayName("Arts martiaux")]
        public virtual List<ArtMartial> ArtMartials { get; set; } = new List<ArtMartial>();
        public int Potentiel
        {
            get
            {
                if (this.Arme == null)
                {
                    return this.Force * (this.ArtMartials.Count + 1);
                }
                else
                {
                    return (this.Force + this.Arme.Degats) * (this.ArtMartials.Count + 1);
                }
            }
        }
    }        
}
