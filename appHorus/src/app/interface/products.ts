// export interface IProducts {
//   id: number,
//   title: string;
//   price: number;
//   description: string;
//   image: string;
//   category: string;
// }

export interface IProducts {
  productoId: number,
  title: string,
  price: number,
  priceAnterior?: number,
  description: string
  imageUrl: string
  categoria: string
}
