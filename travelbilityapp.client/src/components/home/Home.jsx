import NewestAddedPropertiesContainer from "./newest-added-properties-container/Neweset-Added-Properties-Container";
import AccessibilityConteiner from "./accessibility-container/Accessibility-Container";

export default function Home() {
    return (
        <>
            {/* Carousel Start */}
            <div className="container-fluid p-0">
                <div id="header-carousel" className="carousel slide" data-ride="carousel">
                    <div className="carousel-inner">
                        <div className="carousel-item active">
                            <img className="w-100" src="img/mountain.png" alt="Image" />
                            <div className="carousel-caption d-flex flex-column align-items-center justify-content-center">
                                <div className="p-3" style={{ maxWidth: 900 }}>
                                    <h4 className="text-white text-uppercase mb-md-3">
                                        Tours &amp; Travel
                                    </h4>
                                    <h1 className="display-3 text-white mb-md-4">
                                        Let's Discover The World Together
                                    </h1>
                                </div>
                            </div>
                        </div>
                        <div className="carousel-item">
                            <img className="w-100" src="img/city.png" alt="Image" />
                            <div className="carousel-caption d-flex flex-column align-items-center justify-content-center">
                                <div className="p-3" style={{ maxWidth: 900 }}>
                                    <h4 className="text-white text-uppercase mb-md-3">
                                        Tours &amp; Travel
                                    </h4>
                                    <h1 className="display-3 text-white mb-md-4">
                                        Discover Amazing Places With Us
                                    </h1>
                                </div>
                            </div>
                        </div>
                    </div>
                    <a
                        className="carousel-control-prev"
                        href="#header-carousel"
                        data-slide="prev"
                    >
                        <div className="btn btn-dark" style={{ width: 45, height: 45 }}>
                            <span className="carousel-control-prev-icon mb-n2" />
                        </div>
                    </a>
                    <a
                        className="carousel-control-next"
                        href="#header-carousel"
                        data-slide="next"
                    >
                        <div className="btn btn-dark" style={{ width: 45, height: 45 }}>
                            <span className="carousel-control-next-icon mb-n2" />
                        </div>
                    </a>
                </div>
            </div>
            {/* Carousel End */}

            <NewestAddedPropertiesContainer />

            <AccessibilityConteiner />
        </>
    )
};
