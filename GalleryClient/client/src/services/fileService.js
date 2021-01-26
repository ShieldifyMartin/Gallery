import config from "@/config";
import useAlert from "../components/Alert/UseAlert";

function handleFileUpload(e) {
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

export const fileService = {
    handleFileUpload
};
