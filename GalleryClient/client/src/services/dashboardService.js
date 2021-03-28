import config from "@/config";

const getDashboardData = () => {
    try {
        const response = await axios.get(`${config.restAPI}/admin/dashboard/getDashboardInfo`);
        if (response.status >= 200 && response.status < 300) {
            console.log(response);
          return response.data;
        }
      } catch (err) {
        return [];
      }
}


export const dashboardService = {
    getDashboardData
};
