import { Link } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";

const Navbar = () => {
  const { user, logout } = useAuth();
  return (
    <div className="navbar bg-base-100">
      <div className="navbar-start">
        <div className="dropdown">
          <label tabIndex={0} className="btn btn-ghost btn-circle">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="w-5 h-5"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M4 6h16M4 12h16M4 18h7"
              />
            </svg>
          </label>
          <ul
            tabIndex={0}
            className="p-2 mt-3 shadow menu menu-compact dropdown-content bg-base-100 rounded-box w-52"
          >
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/export">Export</Link>
            </li>
            <li>
              <a href="https://github.com/bishal0602/dotnet-cleanarchitecture-ddd-cqrs-sample">
                Github
              </a>
            </li>
          </ul>
        </div>
      </div>
      <div className="navbar-center">
        <Link
          to="/"
          aria-current="page"
          aria-label="Homepage"
          className="px-2 flex-0 btn btn-ghost "
        >
          <div className="inline-flex text-lg font-semibold transition-all duration-200 font-title text-primary md:text-3xl">
            <span className="lowercase text-primary">books</span>
            <span className="uppercase text-base-content">.ReactUI</span>
          </div>
        </Link>
      </div>
      <div className="navbar-end">
        <div className="dropdown dropdown-end">
          <label tabIndex={0} className="btn btn-ghost btn-circle avatar">
            <div className="w-10 rounded-full">
              <img
                src={user ? "/img/default-user.jpg" : "img/blank-user.png"}
                alt="user-profile"
              />
            </div>
          </label>
          <ul
            tabIndex={0}
            className="p-2 mt-3 shadow menu menu-compact dropdown-content bg-base-100 rounded-box w-52"
          >
            <li>
              <Link to="/profile" className="justify-between">
                Profile
              </Link>
            </li>
            {user ? (
              <li>
                <a onClick={() => logout()}>Logout</a>
              </li>
            ) : (
              <li>
                <Link to="/login" className="justify-between">
                  Login
                </Link>
              </li>
            )}
          </ul>
        </div>
      </div>
    </div>
  );
};
export default Navbar;
