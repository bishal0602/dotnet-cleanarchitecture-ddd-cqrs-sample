const Loading = () => {
  return (
    <div className="flex justify-center w-full">
      <div
        className="radial-progress text-primary animate-spin mt-7"
        style={{ "--value": 70 }}
      ></div>
    </div>
  );
};
export default Loading;
