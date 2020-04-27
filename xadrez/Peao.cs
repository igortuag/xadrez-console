using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez partida;

        public Peao(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            this.partida = partida;
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
            bool[,] mat = new bool[Tabuleiro.linhas, Tabuleiro.colunas];
            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.definirValores(Posicao.linha - 1, Posicao.coluna);
                if(Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha - 2, Posicao.coluna);
                Posicao p2 = new Posicao(Posicao.linha - 1, Posicao.coluna);
                if (Tabuleiro.posicaoValida(p2) && livre(p2) && Tabuleiro.posicaoValida(pos) && livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha - 1, Posicao.coluna -1);
                if (Tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha - 1, Posicao.coluna + 1);
                if (Tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #JogadaEspecial en passant
                if (Posicao.linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.linha, Posicao.coluna - 1);
                    if(Tabuleiro.posicaoValida(esquerda) && existeInimigo (esquerda) && Tabuleiro.peca(esquerda) == partida.vuneravelEnPassant)
                    {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.linha, Posicao.coluna + 1);
                    if (Tabuleiro.posicaoValida(direita) && existeInimigo(direita) && Tabuleiro.peca(direita) == partida.vuneravelEnPassant)
                    {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }else
            {
                pos.definirValores(Posicao.linha + 1, Posicao.coluna);
                if (Tabuleiro.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha + 2, Posicao.coluna);
                Posicao p2 = new Posicao(Posicao.linha - 1, Posicao.coluna);
                if (Tabuleiro.posicaoValida(p2) && livre(p2) && Tabuleiro.posicaoValida(pos) && livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha + 1, Posicao.coluna - 1);
                if (Tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(Posicao.linha + 1, Posicao.coluna + 1);
                if (Tabuleiro.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                // #JogadaEspecial en passant
                if (Posicao.linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.linha, Posicao.coluna - 1);
                    if (Tabuleiro.posicaoValida(esquerda) && existeInimigo(esquerda) && Tabuleiro.peca(esquerda) == partida.vuneravelEnPassant)
                    {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.linha, Posicao.coluna + 1);
                    if (Tabuleiro.posicaoValida(direita) && existeInimigo(direita) && Tabuleiro.peca(direita) == partida.vuneravelEnPassant)
                    {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
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
