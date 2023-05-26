import { Route, Routes } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import ProtectedRoute from "./containers/ProtectedRoute";
import Profile from "./pages/Profile";
import Layout from "./containers/Layout";
import Details from "./pages/BookDetails";

const App = () => {
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/books" element={<Home />} />
        <Route path="/books/:id" element={<Details />} />
        <Route path="/login" element={<Login />} />
        <Route
          path="/profile"
          element={
            <ProtectedRoute>
              <Profile />
            </ProtectedRoute>
          }
        />
        <Route path="*">404</Route>
      </Routes>
    </Layout>
  );
};
export default App;
