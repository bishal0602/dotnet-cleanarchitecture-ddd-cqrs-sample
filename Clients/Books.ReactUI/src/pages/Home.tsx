import { useEffect, useState } from "react";
import { getPaginatedBooks } from "../services/bookService";
import { BooksResponse } from "../types/ApiTypes";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import BooksPager from "../components/books/BooksPager";
import ErrorCard from "../components/common/ErrorCard";
import Loading from "../components/common/Loading";

const Home = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [paginatedBooks, setPaginatedBooks] = useState<BooksResponse | null>();

  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  const pageNumber = searchParams.get("pageNumber") ?? `1`;
  const pageSize = searchParams.get("pageSize") ?? "5";

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        setIsLoading(true);
        const data = await getPaginatedBooks(
          parseInt(pageNumber),
          parseInt(pageSize)
        );
        setPaginatedBooks(data);
      } catch (ex) {
        setPaginatedBooks(null);
      } finally {
        setIsLoading(false);
      }
    };
    fetchBooks();
  }, [pageNumber, pageSize]);

  const nextPage = () => {
    setIsLoading(true);
    navigate(`/?pageNumber=${parseInt(pageNumber) + 1}&pageSize=${pageSize}`);
  };
  const previousPage = () => {
    setIsLoading(true);
    navigate(`/?pageNumber=${parseInt(pageNumber) - 1}&pageSize=${pageSize}`);
  };

  if (isLoading) return <Loading />;
  else if (paginatedBooks == null) {
    return <ErrorCard />;
  } else {
    return (
      <div className="flex flex-col justify-center w-full gap-7 mt-7">
        <div className="flex flex-wrap justify-center gap-10">
          {paginatedBooks.books.map((book) => {
            return (
              <div
                key={book.id}
                className="bg-opacity-25 card w-96 sm:w-64 md:w-80 lg:w-96 bg-primary text-primary-content"
              >
                <div className="card-body">
                  <h2 className="card-title text-neutral">{book.title}</h2>
                  <p>{book.description.slice(0, 150)}</p>
                  <p>
                    Authors:{" "}
                    {book.authors.map((author, index) => (
                      <span key={author.id}>
                        {`${author.firstName} ${author.lastName}`}
                        {index !== book.authors.length - 1 && ", "}
                      </span>
                    ))}
                  </p>
                  <div className="justify-end card-actions">
                    <Link to={"books/" + book.id}>
                      <button className="normal-case btn btn-neutral">
                        <p>View more</p>
                      </button>
                    </Link>
                  </div>
                </div>
              </div>
            );
          })}
        </div>

        <BooksPager
          hasPreviousPage={paginatedBooks.pagination.previousPageLink != null}
          hasNextPage={paginatedBooks.pagination.nextPageLink != null}
          handleNext={nextPage}
          handlePrevious={previousPage}
        />
      </div>
    );
  }
};
export default Home;
