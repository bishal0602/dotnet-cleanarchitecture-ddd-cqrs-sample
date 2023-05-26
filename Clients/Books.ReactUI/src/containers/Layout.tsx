import Footer from "../components/common/Footer";
import Navbar from "../components/common/Navbar";

const Layout = ({ children }: React.PropsWithChildren) => {
  return (
    <div className="min-h-[100dvh] flex flex-col">
      <Navbar />
      <main className="container flex flex-grow">{children}</main>
      <Footer />
    </div>
  );
};

export default Layout;
