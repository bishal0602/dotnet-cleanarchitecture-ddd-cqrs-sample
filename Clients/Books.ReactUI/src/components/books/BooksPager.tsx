type BooksPagerProps = {
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  handleNext: () => void;
  handlePrevious: () => void;
};

const BooksPager = ({
  handleNext,
  handlePrevious,
  hasPreviousPage,
  hasNextPage,
}: BooksPagerProps) => {
  return (
    <div className="grid grid-cols-2 btn-group">
      <button
        className="btn btn-outline btn-neutral"
        disabled={!hasPreviousPage}
        onClick={handlePrevious}
      >
        Previous page
      </button>
      <button
        className="btn btn-outline btn-neutral"
        disabled={!hasNextPage}
        onClick={handleNext}
      >
        Next page
      </button>
    </div>
  );
};
export default BooksPager;
