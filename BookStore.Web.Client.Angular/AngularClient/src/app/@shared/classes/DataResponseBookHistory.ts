import { BookHistory } from "../models/BookHistory";

export interface DataResponseBookHistory {
  succeeded: boolean;
  message: string;
  errors: string;
  data: BookHistory;
}
