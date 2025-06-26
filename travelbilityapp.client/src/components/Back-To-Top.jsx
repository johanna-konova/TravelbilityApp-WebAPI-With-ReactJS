export default function BackToTop() {
    const scrollUpHandler = () => window.scrollTo({ top: 0 });

    return (
        <>
            <button className="btn btn-lg btn-primary btn-lg-square back-to-top" onClick={scrollUpHandler}>
                <i className="fa fa-angle-double-up" />
            </button>
        </>
    )
};
