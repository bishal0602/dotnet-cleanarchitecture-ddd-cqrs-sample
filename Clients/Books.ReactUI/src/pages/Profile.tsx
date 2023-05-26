import { useAuth } from "../hooks/useAuth";

const Profile = () => {
  const { user, logout } = useAuth();
  return (
    <div className="flex justify-center w-full text-neutral">
      <div className="card w-96 glass">
        <figure>
          <img src="/img/default-user.jpg" alt="car!" />
        </figure>
        <div className="card-body">
          <h2 className="card-title text-primary-focus">@{user?.username}</h2>
          <p>
            {user?.firstname} {user?.lastname}
          </p>
          <p>{user?.email}</p>
          <div className="justify-end card-actions">
            <button
              className="btn btn-neutral-content"
              onClick={() => logout()}
            >
              Logout
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
export default Profile;
