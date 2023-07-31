<template>
  <div>
    <table class="table table-striped table-hover">
      <thead>
        <tr>
          <th scope="col">Id</th>
          <th scope="col">Username</th>
          <th scope="col">Email</th>
          <th scope="col">Password</th>
          <th scope="col">Role Id</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <th scope="row">-</th>
          <td><input-component/></td>
          <td><input-component/></td>
          <td><input-component/></td>
          <td><input-component type="number"/></td>
          <td>Create</td>
        </tr>
        <tr>
          <th scope="row">1</th>
          <td>Mark</td>
          <td>salade@gmail.com</td>
          <td>****</td>
          <td>3</td>
          <td>Update/Delete</td>
        </tr>
        <tr>
          <th scope="row">2</th>
          <td>Jacob</td>
          <td>tomate@gmail.com</td>
          <td>****</td>
          <td>3</td>
          <td>Update/Delete</td>
        </tr>
        <tr>
          <th scope="row">3</th>
          <td>Larry the Bird</td>
          <td>oignon@gmail.com</td>
          <td>****</td>
          <td>1</td>
          <td>Update/Delete</td>
        </tr>
      </tbody>
      <tfoot>
        <nav aria-label="Page navigation example">
          <ul class="pagination justify-content-end">
            <li class="page-item disabled">
              <a class="page-link" href="#" tabindex="-1" aria-disabled="true"
                >Previous</a
              >
            </li>
            <li class="page-item"><a class="page-link" href="#">1</a></li>
            <li class="page-item"><a class="page-link" href="#">2</a></li>
            <li class="page-item"><a class="page-link" href="#">3</a></li>
            <li class="page-item">
              <a class="page-link" href="#">Next</a>
            </li>
          </ul>
        </nav>
      </tfoot>
    </table>
  </div>
</template>

<script>
import InputComponent from "@/components/global/InputComponent";
export default {
  name: "UsersTab",
  components: {
    InputComponent,
  },
  data() {
    return {};
  },
  methods: {},
  mounted() {},
};
</script>

<style scoped></style>
