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
          <dt>A title of the graph</dt>
          <dd class="percentage percentage-7">
            <span class="text">
              IE 11: 7%
            </span>
          </dd>
          <dd class="percentage percentage-20">
            <span class="text">
              Chrome: 20%
            </span>
          </dd>
          <dd class="percentage percentage-2">
            <span class="text">
              Android 4.4: 2%
            </span>
          </dd>
        </dl>        
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { dashboardService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      postsCount: 0,
      usersCount: 0,
      activities: {},
    });

    watchEffect(async () => {
      const data = await dashboardService.getDashboardData();

      state.postsCount = data.postsCount;
      state.usersCount = data.usersCount;
      state.activities = data.activities;
    });

    return {
      state,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
