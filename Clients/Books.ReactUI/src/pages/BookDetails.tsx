import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getBookDetails } from "../services/bookService";
import { BookDetails } from "../types/ApiTypes";
import ErrorCard from "../components/common/ErrorCard";
import Loading from "../components/common/Loading";

const Details = () => {
  const [book, setBook] = useState<BookDetails | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const params = useParams();
  const bookId = params.id;
  useEffect(() => {
    const fetchBookDetails = async () => {
      if (!bookId) return;
      try {
        setIsLoading(true);
        const data = await getBookDetails(bookId);
        setBook(data);
      } catch (ex) {
        setBook(null);
      } finally {
        setIsLoading(false);
      }
    };
    fetchBookDetails();
  }, [bookId]);

  if (isLoading) return <Loading />;
  if (book == null) return <ErrorCard />;

  return (
    <div className="flex flex-col justify-center w-full gap-7 mt-7">
      <div className="p-4 bg-white rounded-lg shadow-lg">
        <h2 className="mb-2 text-xl font-bold">{book.title}</h2>
        <p className="text-gray-600">{book.description}</p>

        <div className="mt-4">
          <h3 className="mb-2 text-lg font-bold">Authors</h3>
          <ul className="pl-6 list-disc">
            {book.authors.map((author) => (
              <li
                key={author.id}
              >{`${author.firstName} ${author.lastName}`}</li>
            ))}
          </ul>
        </div>

        <div className="mt-4">
          <h3 className="mb-2 text-lg font-bold">Reviews</h3>
          <ul className="pl-6 list-disc">
            {book.reviews.map((review) => (
              <li key={review.id}>
                <strong>{review.username}:</strong> {review.comment}
              </li>
            ))}
          </ul>
        </div>
      </div>
    </div>
  );
};
export default Details;
