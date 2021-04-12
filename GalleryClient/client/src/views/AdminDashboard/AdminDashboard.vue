<template>
  <div class="dashboard">
    <ul class="routes">
      <li>
        <router-link :to="{ name: 'Home', params: { isAdminRoute: true } }"
          >All Posts</router-link
        >
      </li>
      <li><router-link to="/users">All Users</router-link></li>
    </ul>
    <div class="main">
      <div class="couters">
        <div>
          <h2>{{ state.postsCount }}</h2>
          <span>posts count</span>
        </div>
        <div>
          <h2>{{ state.usersCount }}</h2>
          <span>users count</span>
        </div>
      </div>
      <div class="stats">
        <dl>
          <dt>Months Activities Statistics</dt>
          <dd
            v-for="(activityPercentage, month) in state.activities"
            :key="activityPercentage"
            :class="getChartClass(activityPercentage)"
          >
            <span class="text"> {{ month }}: {{ activityPercentage }}% </span>
          </dd>
        </dl>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { dashboardService } from "../../services";
import { getMonthString } from "./service";

export default defineComponent({
  setup() {
    const state = reactive({
      postsCount: 0,
      usersCount: 0,
      activities: {},
    });

    const getChartClass = (activityPercentage) => {
      return `percentage percentage-${activityPercentage}`;
    };

    watchEffect(async () => {
      const data = await dashboardService.getDashboardData();
      const activitiesObj = {};
      for (const property in data.chartData) {
        const currentMonth = getMonthString(property);
        activitiesObj[currentMonth] = data.chartData[property];
      }

      state.postsCount = data.postsCount;
      state.usersCount = data.usersCount;
      state.activities = activitiesObj;
    });

    return {
      state,
      getChartClass,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
