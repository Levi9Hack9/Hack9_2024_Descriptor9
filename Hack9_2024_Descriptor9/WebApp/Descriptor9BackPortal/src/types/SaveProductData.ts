import { Langs } from "./Product";

export interface SaveProductData {
  title: string;
  descriptionTranslations: Langs;
  category: string;
}
