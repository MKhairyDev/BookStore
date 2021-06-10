import { Book } from "../models/Book";

export interface DataResponseBook {
  succeeded: boolean;
  message: string;
  errors: string;
  data: Book;
}
