import axios from "axios";
import {
  Book,
  BookDetails,
  BooksResponse,
  FileExportModel,
  Pagination,
} from "../types/ApiTypes";

const API_SERVER = "http://localhost:5263";

export const getPaginatedBooks = async (
  pageNumber: number,
  pageSize: number
): Promise<BooksResponse> => {
  const response = await axios.get(
    `${API_SERVER}/api/books?pageSize=${pageSize}&pageNumber=${pageNumber}`
  );
  if (response.status === 200) {
    const books = response.data as Book[];
    const pagination = response.headers["x-pagination"];
    const paginationObject = JSON.parse(pagination) as Pagination;
    return { books: books, pagination: paginationObject };
  }
  throw new Error("Error getting books");
};

export const getBookDetails = async (id: string): Promise<BookDetails> => {
  const response = await axios.get(`${API_SERVER}/api/books/${id}`);
  if (response.status === 200) {
    const book = response.data as BookDetails;
    return book;
  }
  throw new Error("Error getting book");
};

export const downloadBooksCsv = async (accessToken:string) : Promise<FileExportModel>=> {
  const response = await axios.get(`${API_SERVER}/api/books/export`, {
    responseType: "blob",
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });

  const contentDisposition = response.headers["content-disposition"];
  const fileNameMatch = contentDisposition.match((/filename\*?=['"]?(?:UTF-\d['"]*)?([^;\r\n"']*)['"]?;?/i));
  const fileName = fileNameMatch ? fileNameMatch[1] : "books.csv";
  const contentType = response.headers["content-type"];
  if (contentType !== "text/csv") {
    throw new Error("The server did not return a CSV file.");
  }
return {data: response.data, fileName: fileName, contentType: contentType};

};