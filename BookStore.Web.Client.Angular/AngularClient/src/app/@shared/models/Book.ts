import { Author } from "./Author";

export interface Book {
    id: number;
    title: string;
    shortDescription: string;
    publishDate: Date;
    authors: Author[];
  }