using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public int Numero;
        public string Titular;
        public double DepositoInicial;
        public double SaldoConta;
        public double Taxa = 3.50;

        public ContaBancaria(int numero, string titular, double depositoInicial = 0)
        {
            Numero = numero;
            Titular = titular;
            DepositoInicial = depositoInicial;
            SaldoConta = depositoInicial;
        }

        public void Deposito(double quantia)
        {
            this.SaldoConta += quantia;
        }

        public void Saque(double quantia)
        {
            this.SaldoConta -= (quantia + Taxa);
        }
    }
}