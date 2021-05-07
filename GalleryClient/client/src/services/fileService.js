import config from "@/config";
import useAlert from "../components/Alert/UseAlert";

const handleFileUpload = (e) => {
    let file = e.target.files[0];
    if (!e.target.files.length) return;
    if (e.target.files.length === 1) {
        if (file.size > config.maxImageSize) {
            useAlert("Too large picture!", false);
        } else {
            return file;
        }
    } else {
        useAlert("Only one photo is allowed!", false);
    }
}

const returnFileFromUrl = async (pictureUrl) => {
    const url = pictureUrl;
    const fileName = "file.jpg";

    const response = await fetch(url);
    const contentType = response.headers.get("content-type");
    const blob = await response.blob();
    const file = new File([blob], fileName, { type: contentType });
    return file;
}

export const fileService = {
    handleFileUpload,
    returnFileFromUrl
};
