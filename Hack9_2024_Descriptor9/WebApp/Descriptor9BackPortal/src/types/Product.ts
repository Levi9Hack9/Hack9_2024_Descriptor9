export interface Product {
  id: string;
  title: string;
  descriptionTranslations: Langs;
  category: string;
  imageUrl: string;
}

export interface Langs {
  english: string;
  spanish: string;
  dutch: string;
  ukrainian: string;
  romanian: string;
  serbian: string;
}
