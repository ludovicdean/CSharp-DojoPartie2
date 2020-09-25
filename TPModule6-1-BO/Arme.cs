
using System.ComponentModel;

namespace TpModule6Bo
{
    public class Arme : Ids
    {
        public override int Id { get; set; }
        public override string Nom { get; set; }
        [DisplayName("Dégâts")]
        public int Degats { get; set; }
    }
}