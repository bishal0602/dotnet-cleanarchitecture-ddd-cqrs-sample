type ErrorCardProps = {
  message?: string;
};

const ErrorCard = ({ message }: ErrorCardProps) => {
  return (
    <div className="shadow-lg h-fit alert alert-error">
      <div>
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="flex-shrink-0 w-6 h-6 stroke-current"
          fill="none"
          viewBox="0 0 24 24"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth="2"
            d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z"
          />
        </svg>
        <span>{message ?? "Error! Something went wrong"}</span>
      </div>
    </div>
  );
};
export default ErrorCard;
