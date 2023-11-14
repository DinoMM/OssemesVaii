namespace OSsemes.Data
{
    public class Zakaznik
    {
        public long ID { get; set; }
        public string Meno { get; set; }
        public string Priezvisko { get; set; }
        public string Email { get; set; }
        public DateTime DatumPrichodu { get; set; }
        public int CisloIzby { get; set; }
        public int PocetDni { get; set; }
    }
}
