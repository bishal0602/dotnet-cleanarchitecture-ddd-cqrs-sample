import { useState } from "react";
import { useAuth } from "../hooks/useAuth";
import { downloadBooksCsv } from "../services/bookService";

export function Export() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState("");
  const {user} = useAuth();

  const handleDownload = async () => {
    setIsLoading(true);
    setError("");
    try {
      if(!user)return;
      const file = await downloadBooksCsv(user.token);
      const url = window.URL.createObjectURL(new Blob([file.data]));
      const link = document.createElement("a");
      link.href = url;
      link.setAttribute("download", file.fileName);
      document.body.appendChild(link);
      link.click();
    } catch (error) {
      setError("An error occurred while downloading the CSV file.");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="flex justify-center w-full pt-7">
      <div className="w-fit max-w-md">
      <h1 className="text-3xl font-bold mb-4 text-primary">Export Books to <span className="text-neutral">CSV</span></h1>
      <p className="text-neutral mb-8">
        Click the button below to download a CSV file containing all books.
      </p>
      <button
        className="btn btn-neutral"
        onClick={handleDownload}
        disabled={isLoading}
      >
        {isLoading ? "Downloading..." : "Download CSV"}
      </button>
      {error && <p className="text-red-500 mt-4">{error}</p>}
    </div>
    </div>
  );
}