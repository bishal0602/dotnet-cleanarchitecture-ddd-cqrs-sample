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
    <div className="flex flex-col justify-center w-full gap-7 mt-7" 
    style={{
      background: "linear-gradient(180deg, #F6F8FA 0%, rgba(246, 248, 250, 0) 100%), linear-gradient(180deg, #FFFFFF 0%, rgba(255, 255, 255, 0) 100%)",
      boxShadow: "0px 4px 20px rgba(0, 0, 0, 0.1)",
      borderRadius: "20px",
      padding: "30px",
    }}
    >
      <div className="p-4 bg-transparent rounded-lg shadow-lg">
        <h2 className="mb-2 text-3xl font-bold">{book.title}</h2>
        <p className="text-gray-600">{book.description}</p>

        <div className="mt-8">
          <h3 className="mb-2 text-2xl font-bold">Authors</h3>
          <ul className="pl-6 list-disc">
            {book.authors.map((author) => (
              <li
                key={author.id}
                className="text-lg font-medium text-gray-800"
              >{`${author.firstName} ${author.lastName}`}</li>
            ))}
          </ul>
        </div>

        <div className="mt-8">
          <h3 className="mb-2 text-2xl font-bold">Reviews</h3>
          <ul className="pl-6 ">
            {book.reviews.map((review, index) => (
              <li key={review.id} className="mt-4">
                <div className="flex items-center">
                  <div className="flex-shrink-0">
                    <img
                      className="w-10 h-10 rounded-full"
                      src={`/img/user-${index%2+1}.jpg`}
                      alt={`${review.username}'s avatar`}
                    />
                  </div>
                  <div className="ml-4">
                    <h4 className="text-lg font-medium text-gray-800">
                      {review.username}
                    </h4>
                    <p className="text-gray-600">{review.comment}</p>
                  </div>
                </div>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </div>
  );
};
export default Details;
