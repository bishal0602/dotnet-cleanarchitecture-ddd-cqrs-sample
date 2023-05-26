import { createContext, useContext, useMemo } from "react";
import { useLocalStorage } from "./useLocalStorage";
import { useNavigate } from "react-router-dom";
import { User } from "../types/AuthTypes";
import { Optional } from "../utils/TsExtensions";

type AuthStateContextProps = {
  user: Optional<User>;
  login: (data: User) => Promise<void>;
  logout: () => Promise<void>;
};
const AuthStateContext = createContext<AuthStateContextProps>(
  {} as AuthStateContextProps
);

export const AuthProvider = ({ children }: React.PropsWithChildren) => {
  const [user, setUser] = useLocalStorage<Optional<User>>(
    "booksreactui:user",
    null
  );
  const navigate = useNavigate();

  const login = async (data: User) => {
    setUser(data);
    navigate("/");
  };

  const logout = async () => {
    setUser(null);
    navigate("/");
  };

  const value = useMemo(
    () => ({
      user,
      login,
      logout,
    }),
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [user]
  );

  return (
    <AuthStateContext.Provider value={value}>
      {children}
    </AuthStateContext.Provider>
  );
};

// eslint-disable-next-line react-refresh/only-export-components
export const useAuth = () => useContext(AuthStateContext);
