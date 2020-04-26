using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro tab) : base(cor, tab)
        {

        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tabuleiro.peca(pos);
            return p == null || p.Cor != Cor;
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = Tabuleiro.peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool livre(Posicao pos)
        {
            return Tabuleiro.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.definirValores(Posicao.linha - 1, Posicao.coluna);
                if(Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha - 2, Posicao.coluna);
                if (Tabuleiro.posicaoValida(pos) && livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha - 1, Posicao.coluna -1);
                if (Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha - 1, Posicao.coluna + 1);
                if (Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
            }else
            {
                pos.definirValores(Posicao.linha + 1, Posicao.coluna);
                if (Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha + 2, Posicao.coluna);
                if (Tabuleiro.posicaoValida(pos) && livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha + 1, Posicao.coluna - 1);
                if (Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha + 1, Posicao.coluna + 1);
                if (Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
