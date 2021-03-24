namespace ArvoreAVL 
{
	class No
	{
		public No noEsquerdo;
		
		//public char info;
		public int info; // arvore avl
		
		public No noDireito;

		// arvore avl - fator de balaceamento
		public int fatorBalanceamento;

		//public No(char info)
		public No(int info) // arvore avl
		{
			this.noEsquerdo = null;
			this.info = info;
			this.noDireito = null;
			this.fatorBalanceamento = 0; // arvore avl
		}
	}
}