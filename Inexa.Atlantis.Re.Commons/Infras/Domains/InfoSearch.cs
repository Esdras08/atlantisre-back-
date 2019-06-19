namespace Inexa.Atlantis.Re.Commons.Infras.Domains
{
    /// <summary>
    /// Permet de spécifier les caractéristiques d'une recherche
    /// </summary>
    public class InfoSearch<T>
    {
        /// <summary>
        /// Effectuer la recherche sur le critère correspondant
        /// </summary>
        public bool Consider { get; set; }

        /// <summary>
        /// Effectuer un 'order by' sur le critère correspondant
        /// </summary>
        public bool IsOrderByField { get; set; }

        /// <summary>
        /// Effectuer un 'sum' sur le critère correspondant
        /// </summary>
        public bool IsSumField { get; set; }

        /// <summary>
        /// Contient la somme
        /// </summary>
        public decimal Sum { get; set; }

        private Intervalle<T> _intervalle;

        public Intervalle<T> Intervalle
        {
            get
            {
                return _intervalle;
            }
            set
            {
                _intervalle = value;
                Consider = _intervalle != null;
            }
        }



        public string OperatorToUse { get; set; }

        public InfoSearch()
        {
            Consider = false;
            Intervalle = null;
        }
    }
}
